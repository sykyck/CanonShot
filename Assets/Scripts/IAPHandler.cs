using UnityEngine;
using System.Collections;
using UnityEngine.Purchasing;

public class IAPHandler : IStoreListener {

	public IStoreController controller;
	public IExtensionProvider extensions;
	public bool IsIAPInitialised,IsIAPCompleted,IsIAPFailed;

	private static IAPHandler instance;

	public static IAPHandler GetInstance()
	{
		if (instance == null)
		{
			instance = new IAPHandler();
		}
		return instance;
	}

	private IAPHandler () {
		IsIAPInitialised = false;
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct(IAPConstants.COINS_100, ProductType.Consumable, new IDs
			{
				{IAPConstants.COINS_100, GooglePlay.Name},
				{IAPConstants.COINS_100, MacAppStore.Name}
			});
		builder.AddProduct(IAPConstants.COINS_200, ProductType.Consumable, new IDs
			{
				{IAPConstants.COINS_200, GooglePlay.Name},
				{IAPConstants.COINS_200, MacAppStore.Name}
			});
		builder.AddProduct(IAPConstants.COINS_500, ProductType.Consumable, new IDs
			{
				{IAPConstants.COINS_500, GooglePlay.Name},
				{IAPConstants.COINS_500, MacAppStore.Name}
			});
		UnityPurchasing.Initialize (this, builder);
	}

	/// <summary>
	/// Called when Unity IAP is ready to make purchases.
	/// </summary>
	public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
	{
		this.controller = controller;
		this.extensions = extensions;
		Debug.Log ("IAPPurchaseInitialized");
		IsIAPInitialised = true;
	}

	/// <summary>
	/// Called when Unity IAP encounters an unrecoverable initialization error.
	///
	/// Note that this will not be called if Internet is unavailable; Unity IAP
	/// will attempt initialization until it becomes available.
	/// </summary>
	public void OnInitializeFailed (InitializationFailureReason error)
	{
       

	}
	/// <summary>
	/// Called when a purchase completes.
	///
	/// May be called at any time after OnInitialized().
	/// </summary>
	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs e)
	{
		Debug.Log ("IAPPurchaseCompleted");
		IsIAPCompleted = true;
		return PurchaseProcessingResult.Complete;
	}

	/// <summary>
	/// Called when a purchase fails.
	/// </summary>
	public void OnPurchaseFailed (Product i, PurchaseFailureReason p)
	{
		Debug.Log ("IAPPurchaseFailed");
		IsIAPFailed = true;
	}
		
}
