let transactions = [];
let connection;
let transactionIdToUpdate = -1;
GetTransactionData();
//setupSignalR();

// Get & Display Exchange Data
async function GetTransactionData() {
    await fetch('http://localhost:33531/transaction')
        .then(x => x.json())
        .then(y => {
            transactions = y.$values;
            //console.log(exchanges);
            DisplayTransactionData();
        });
}
function DisplayTransactionData() {
    document.getElementById("transactionHolder").innerHTML = "";
    transactions.forEach(t => {
        document.getElementById("transactionHolder").innerHTML +=
            `
            <tr>
                <th scope="row">${t.id}</th>
                <td>${formatDate(t.date)}</td>
                <td>${t.stockId}</td>
                <td>${t.amount}</td>
                <td>
                    <button type="button" class="btn btn-secondary" onclick="ModifyTransaction('${t.id}')">Edit</button>
                    <button type="button" class="btn btn-danger" onclick="DeleteTransaction('${t.id}')">Delete</button>
                </td>
            </tr>
            `;
    });
}

// CRUD Exchange
function CreateNewTransaction() {
    // Get data from form inputs
    let id = GetUniqueExchangeId(100);
    let ticker = document.getElementById('newExcTicker').value;
    let name = document.getElementById('newExcName').value;
    let region = document.getElementById('newExcRegion').value;
    // Send Create call to API
    fetch('http://localhost:33531/exchange', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { id: id, nameShort: ticker, name: name, region: region })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            GetExchangeData();
        })
        .catch(error => { console.log('Error: ', error) });
    // Clear form inputs
    document.getElementById('newExcTicker').value = "";
    document.getElementById('newExcName').value = "";
    document.getElementById('newExcRegion').value = "";
}
function ModifyTransaction(id) {
    document.getElementById("f_editExchange").classList.remove("d-none");
    let e = GetExchangeById(id);
    document.getElementById("modExcTicker").value = e.nameShort;
    document.getElementById("modExcName").value = e.name;
    document.getElementById("modExcRegion").value = e.region;
    exchangeIdToUpdate = e.id;
    //console.log(exchangeIdToUpdate);
}
function UpdateTransaction() {
    // Get data from form inputs
    let ticker = document.getElementById('modExcTicker').value;
    let name = document.getElementById('modExcName').value;
    let region = document.getElementById('modExcRegion').value;
    // Send Update call to API
    fetch('http://localhost:33531/exchange', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { id: exchangeIdToUpdate, nameShort: ticker, name: name, region: region })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            GetExchangeData();
        })
        .catch(error => { console.log('Error: ', error) });
    // Hide Edit Form
    document.getElementById("f_editExchange").classList.add("d-none");
}
function DeleteTransaction(id) {
    fetch('http://localhost:33531/exchange/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            GetExchangeData();
        })
        .catch((error) => { console.error('Error: ', error) });
}

// SignalR
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:33531/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TransactionCreated", (user, message) => {
        GetTransactionData();
    });

    connection.on("TransactionDeleted", (user, message) => {
        GetTransactionData();
    });

    connection.on("TransactionUpdated", (user, message) => {
        GetTransactionData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (error) {
        console.log(error);
        setTimeout(start, 5000);
    }
}

// Other FXs
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}
function GetTransactionById(id) {
    return transactions.filter(t => t.id == id)[0];
}