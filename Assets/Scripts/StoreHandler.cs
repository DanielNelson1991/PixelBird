using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

public class StoreHandler : MonoBehaviour, IStoreListener
{
    private IStoreController controller;    // Required IStoreController for purchases
    private IExtensionProvider extensions;  // Required IExtensionProvider for purchases

    void Start()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("com.fairygaming.150coins", ProductType.Consumable);
        builder.AddProduct("com.fairygaming.250coins", ProductType.Consumable);
        builder.AddProduct("com.fairygaming.500coins", ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    /// <summary>
    /// Called when Unity IAP encounters an unrecoverable initialization error.
    ///
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    private bool IsInitalized()
    {
        return controller != null && extensions != null;
    }

    public void Purchase(string code)
    {
        try
        {
            BuyProductID(code);
        } catch (System.Exception e)
        {
            Debug.Log("BuyProductFailed: Exception thrown " + e);
        }
    }

    private void BuyProductID(string productID)
    {
        if(!IsInitalized())
        {
            return;
        }

        Product product = controller.products.WithID(productID);
        controller.InitiatePurchase(product);
    }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        Product pp;
        pp = e.purchasedProduct;
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        // Do nothing
    }
    
    public PurchaseProcessingResult ProcessTransaction (PurchaseEventArgs e)
    {
        CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), null, Application.identifier);

        try
        {
            var result = validator.Validate(e.purchasedProduct.receipt);
            foreach(IPurchaseReceipt productReceipt in result)
            {
                
            }
        } catch (IAPSecurityException)
        {
            Debug.Log("Invalid");
        }

        return PurchaseProcessingResult.Complete;
    }

    private void PurchaseSuccessful(Product pp)
    {
        if(pp.definition.type == ProductType.Consumable)
        {
            Debug.Log("Purchase has been successful, consumable, routing " + pp.definition.id);
        }
    }

    public void RetrievePurchases()
    {
        
    }

    public void SuccessConumable(string code)
    {
        if(code == "com.fairygaming.150coins")
        {
            GameManagerNew._g_Coins += 150;
            Debug.Log("MADE");
        }
    }
}
