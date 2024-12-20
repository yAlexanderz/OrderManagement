import React, { useState } from "react";
import axios from "axios";
import "./PurchasePage.css";

const mockCustomerId = "123e4567-e89b-12d3-a456-426614174000";
const mockProducts = [
  { id: "1", name: "Smartphone XYZ", price: 799.99, description: "High-end smartphone with excellent camera" },
  { id: "2", name: "Wireless Headphones", price: 199.99, description: "Noise-cancelling over-ear headphones" },
  { id: "3", name: "Laptop Pro 15", price: 1299.99, description: "Powerful laptop for professionals" },
];

function PurchasePage() {
  const [orderItems, setOrderItems] = useState([]);
  const [responseMessage, setResponseMessage] = useState("");

  const handleAddProduct = (product) => {
    const existingItem = orderItems.find((item) => item.productId === product.id);
    if (existingItem) {
      setOrderItems(
        orderItems.map((item) =>
          item.productId === product.id
            ? { ...item, quantity: item.quantity + 1 }
            : item
        )
      );
    } else {
      setOrderItems([
        ...orderItems,
        { productId: product.id, productName: product.name, quantity: 1, unitPrice: product.price },
      ]);
    }
  };

  const handleRemoveProduct = (productId) => {
    setOrderItems(orderItems.filter((item) => item.productId !== productId));
  };

  const handleSubmitOrder = async () => {
    if (orderItems.length === 0) {
      alert("Please add at least one product to the order.");
      return;
    }

    const order = {
      customerId: mockCustomerId,
      items: orderItems,
    };

    try {
      const response = await axios.post("http://10.131.9.82:7107/api/orders/PlaceOrder", order);
      setResponseMessage(`Order placed successfully! Order ID: ${response.data.orderId}`);
    } catch (error) {
      setResponseMessage(`Error placing order: ${error.message}`);
    }
  };

  return (
    <div className="purchase-page">
      <header className="header">
        <h1>ShopEasy</h1>
      </header>

      <div className="content">
        <h2>Products</h2>
        <div className="products">
          {mockProducts.map((product) => (
            <div className="product-card" key={product.id}>
              <h3>{product.name}</h3>
              <p>{product.description}</p>
              <p><strong>${product.price.toFixed(2)}</strong></p>
              <button onClick={() => handleAddProduct(product)}>Add to Cart</button>
            </div>
          ))}
        </div>

        <h2>Your Cart</h2>
        <div className="cart">
          {orderItems.length === 0 ? (
            <p>Your cart is empty.</p>
          ) : (
            <ul>
              {orderItems.map((item) => (
                <li key={item.productId}>
                  {item.productName} - Quantity: {item.quantity} - Total: $
                  {(item.unitPrice * item.quantity).toFixed(2)}{" "}
                  <button onClick={() => handleRemoveProduct(item.productId)}>Remove</button>
                </li>
              ))}
            </ul>
          )}
        </div>

        <button className="submit-button" onClick={handleSubmitOrder}>
          Place Order
        </button>

        {responseMessage && <p className="response-message">{responseMessage}</p>}
      </div>
    </div>
  );
}

export default PurchasePage;
