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
            //console.log(stocks);
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
                    <button type="button" class="btn btn-danger" onclick="DeleteStock('${t.id}')">Delete</button>
                </td>
            </tr>
            `;
    });
}

// CRUD Stock
function CreateNewStock() {
    // Get data from form inputs
    let id = GetUniqueStockId(100);
    let ticker = document.getElementById('newStockTicker').value;
    let company = document.getElementById('newStockName').value;
    let excId = document.getElementById('newStockExcId').value;
    let dividend = document.getElementById('newStockDividend').value;
    let price = document.getElementById('newStockPrice').value;

    // Send Create call to API
    fetch('http://localhost:33531/stock', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { id: id, company: company, ticker: ticker, price: price, dividend: dividend, exchangeId: excId })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            GetStockData();
        })
        .catch(error => { console.log('Error: ', error) });
    // Clear form inputs
    document.getElementById('newStockTicker').value = "";
    document.getElementById('newStockName').value = "";
    document.getElementById('newStockExcId').value = "";
    document.getElementById('newStockDividend').value = "";
    document.getElementById('newStockPrice').value = "";
}
function DeleteStock(id) {
    fetch('http://localhost:33531/stock/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            GetStockData();
        })
        .catch((error) => { console.error('Error: ', error) });
}

// Other FXs
function GetUniqueStockId(max) {
    let id = -1;
    do {
        id = Math.floor(Math.random() * max) + 1;
    } while (stocks.filter(x => x.id == id).length > 0);
    return id;
}