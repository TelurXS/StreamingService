
window.initializePayPal = () => {

    let selected = document.getElementById("selected-subscription").value;

    if (selected == "")
        return;

    paypal.Buttons({
        style: {
            layout: 'vertical',
            color: 'silver',
            tagline: 'false'
        },
        createOrder: (data, actions) => {
            return fetch(`/api/payment/${selected}`, { method: "post", })
                .then((response) => response.json())
                .then((order) => order.id)
        },
        onApprove: (data, actions) => {
            return fetch(`/api/payment?orderId=${data.orderID}`, { method: "get", })
                .then((response) => response.json())
                .then((data) => window.alert("Success " + JSON.stringify(data)));
        }
    }).render('#paypal-button-container');
}