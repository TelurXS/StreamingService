
const SUBSCRIPTIONS = ["Standart", "Premium"]

window.initializePayPal = () => {

    let selected = document.getElementById("selected-subscription").value;

    if (selected == "" || SUBSCRIPTIONS.includes(selected) == false)
        return;

    paypal.Buttons({
        style: {
            layout: 'vertical',
            color: 'silver',
            tagline: 'false',
            shape: 'pill',
        },
        createOrder: (data, actions) => {
            return fetch(`/api/payment/${selected}`, { method: "post", })
                .then((response) => response.json())
                .then((order) => order.id)
        },
        onApprove: (data, actions) => {
            return fetch(`/api/payment?orderId=${data.orderID}`, { method: "get", })
                .then((response) => response.json())
                .then((data) => {

                    const urlParams = new URLSearchParams(window.location.search);
                    const returnUrl = urlParams.get('returnUrl');

                    if (returnUrl != "") {
                        window.location.href = returnUrl
                        return;
                    }

                    window.location.href = "/registration-success"
                });
        }
    }).render('#paypal-button-container');
}