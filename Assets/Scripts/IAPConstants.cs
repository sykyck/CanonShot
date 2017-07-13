public class IAPConstants 
{
	private static IAPConstants instance;

	private IAPConstants() {}

	public static IAPConstants GetInstance()
	{
		if (instance == null)
		{
			instance = new IAPConstants();
		}
		return instance;
	}
	#region COINS RELATED INAPP PURCHASES
	public const string COINS_100 = "100_gold_coins";
	public const string COINS_200 = "200_gold_coins";
	public const string COINS_500 = "500_gold_coins";
	#endregion
}
