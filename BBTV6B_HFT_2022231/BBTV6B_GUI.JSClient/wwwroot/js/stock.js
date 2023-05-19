let stocks = [];
let connection;
let stockIdToUpdate = -1;
GetStockData();
//setupSignalR();

// Get & Display Stock Data
async function GetStockData() {
    await fetch('http://localhost:33531/stock')
        .then(x => x.json())
        .then(y => {
            stocks = y.$values;
            console.log(stocks);
            DisplayStockData();
        });
}
function DisplayStockData() {
    document.getElementById("stockHolder").innerHTML = "";
    stocks.forEach(t => {
        document.getElementById("stockHolder").innerHTML +=
            `
            <tr>
                <th scope="row">${t.ticker}</th>
                <td>${t.company}</td>
                <td>${t.exchangeId}</td>
                <td>${t.dividend}</td>
                <td>${t.price}</td>
                <td>
                    <button type="button" class="btn btn-primary" onclick="ModifyExchange('${t.id}')">Buy</button>
                    <button type="button" class="btn btn-warning" onclick="ModifyExchange('${t.id}')">Edit</button>
                    <button type="button" class="btn btn-danger" onclick="DeleteExchange('${t.id}')">Delete</button>
                </td>
            </tr>
            `;
    });
}