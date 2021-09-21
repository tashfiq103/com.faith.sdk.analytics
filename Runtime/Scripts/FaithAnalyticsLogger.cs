namespace com.faith.sdk.analytics
{
    using UnityEngine;
    using System.Collections.Generic;

    public static class FaithAnalyticsLogger
    {
        #region Private Variables

        private static FaithAnalyticsGeneralConfiguretionInfo _faithAnalyticsGeneralConfiguretionInfo;

		#endregion

		#region Configuretion

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnGameStart()
		{
			_faithAnalyticsGeneralConfiguretionInfo = Resources.Load<FaithAnalyticsGeneralConfiguretionInfo>("FaithAnalyticsGeneralConfiguretionInfo");
		}

		private static string DecimalToHexNumeric(int value)
		{

			if (value == -1)
				return ".";
			else if (value == 10)
				return "A";
			else if (value == 11)
				return "B";
			else if (value == 12)
				return "C";
			else if (value == 13)
				return "D";
			else if (value == 14)
				return "E";
			else if (value == 15)
				return "F";
			else
				return value.ToString();
		}

		private static string GetHexValue(float value, bool considerDecimalPoint = false)
		{

			if (value == 0)
				return "0";

			Stack<float> stackForDecimal = new Stack<float>();

			while (value > 0)
			{

				if (value < 16)
				{

					float floatValue = value % 16;
					stackForDecimal.Push((int)floatValue);

					if (!considerDecimalPoint)
					{
						value = 0;
						break;
					}
					else
					{

						//Decimal Fraction
						//----------------
						stackForDecimal.Push(-1);
						float decimalFraction = floatValue - ((int)floatValue);
						if (decimalFraction > 0)
							stackForDecimal.Push((int)(decimalFraction * 16));
						//----------------

						value = 0;
					}
				}
				else
				{

					stackForDecimal.Push(value % 16);
					value /= 16;
				}
			}

			string result = "";
			while (stackForDecimal.Count > 0)
			{
				int integerValue = (int)stackForDecimal.Pop();
				string hexValue = DecimalToHexNumeric(integerValue);
				result += hexValue;
			}

			string[] splitByDecimalPoint = result.Split('.');
			if (splitByDecimalPoint.Length > 1)
				return splitByDecimalPoint[1] + "." + splitByDecimalPoint[0];
			else
				return splitByDecimalPoint[0];
		}

		private static string GetHexColorFromRGBColor(Color color)
		{

			Vector3 _32BitColor = new Vector3(
				color.r * 255,
				color.g * 255,
				color.b * 255);

			return "#"
				+ (_32BitColor.x < 16 ? "0" : "") + GetHexValue(_32BitColor.x)
				+ (_32BitColor.y < 16 ? "0" : "") + GetHexValue(_32BitColor.y)
				+ (_32BitColor.z < 16 ? "0" : "") + GetHexValue(_32BitColor.z);
		}

		#endregion

		#region Public Callback

		public static void Log(object message)
		{
			if (_faithAnalyticsGeneralConfiguretionInfo.ShowAPSdkLogInConsole)
			{
				Debug.Log(string.Format(
					"<color={0}>{1}{2}</color>",
					GetHexColorFromRGBColor(_faithAnalyticsGeneralConfiguretionInfo.InfoLogColor),
					"[" + FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "]: ",
					message));
			}

		}


		public static void LogWarning(object message)
		{
			if (_faithAnalyticsGeneralConfiguretionInfo.ShowAPSdkLogInConsole)
			{
				Debug.LogWarning(string.Format(
					"<color={0}>{1}{2}</color>",
					GetHexColorFromRGBColor(_faithAnalyticsGeneralConfiguretionInfo.WarningLogColor),
					"[" + FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "]: ",
					message));
			}

		}

		public static void LogError(object message)
		{
			if (_faithAnalyticsGeneralConfiguretionInfo.ShowAPSdkLogInConsole)
			{
				Debug.LogError(string.Format(
					"<color={0}>{1}{2}</color>",
					GetHexColorFromRGBColor(_faithAnalyticsGeneralConfiguretionInfo.ErrorLogColor),
					"[" + FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "]: ",
					message));
			}

		}

		#endregion

	}
}

