namespace ShoppingCart;

public enum CartStatus
{
    Unset = 0,
    Opened = 1,
    Confirmed = 2,
    CheckedOut = 4,
    Expired = 8
}
