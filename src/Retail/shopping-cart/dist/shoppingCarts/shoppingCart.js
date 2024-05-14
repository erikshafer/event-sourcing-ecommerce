export const getInitialState = () => {
    return {
        status: "Empty",
    };
};
/////////////////////////////////////////
////////// Evolve
/////////////////////////////////////////
export const evolve = (state, event) => {
    const { type, data } = event;
    switch (type) {
        case "ProductItemAddedToShoppingCart":
        case "ProductItemRemovedFromShoppingCart": {
            if (state.status !== "Opened" && state.status !== "Empty")
                return state;
            const { productItem: { productId, quantity }, } = data;
            const productItems = state.status === "Opened"
                ? state.productItems
                : new Map();
            const plusOrMinus = type == "ProductItemAddedToShoppingCart" ? 1 : -1;
            return {
                status: "Opened",
                productItems: withUpdatedQuantity(productItems, productId, plusOrMinus * quantity),
            };
        }
        case "ShoppingCartConfirmed":
        case "ShoppingCartCancelled":
            return { status: "Closed" };
        default:
            return state;
    }
};
const withUpdatedQuantity = (current, productId, quantity) => {
    const productItems = new Map(current);
    const currentQuantity = productItems.get(productId) ?? 0;
    productItems.set(productId, currentQuantity + quantity);
    return productItems;
};
