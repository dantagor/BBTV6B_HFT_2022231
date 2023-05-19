let exchanges = [];
let connection;
let exchangeIdToUpdate = -1;
GetExchangeData();

// Get & Display Exchange Data
async function GetExchangeData() {
    await fetch('http://localhost:33531/exchange')
        .then(x => x.json())
        .then(y => {
            exchanges = y.$values;
            //console.log(exchanges);
            DisplayExchangeData();
        });
}
function DisplayExchangeData() {
    document.getElementById("exchangeHolder").innerHTML = "";
    exchanges.forEach(t => {
        document.getElementById("exchangeHolder").innerHTML +=
            `
            <tr>
                <th scope="row">${t.id}</th>
                <td>${t.nameShort}</td>
                <td>${t.name}</td>
                <td>${t.region}</td>
                <td>
                    <button type="button" class="btn btn-secondary" onclick="ModifyExchange('${t.id}')">Edit</button>
                    <button type="button" class="btn btn-danger" onclick="DeleteExchange('${t.id}')">Delete</button>
                </td>
            </tr>
            `;
    });
}

// CRUD Exchange
function CreateNewExchange() {
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
function ModifyExchange(id) {

}

// Other FXs
function GetUniqueExchangeId(max) {
    let id = -1;
    do {
        id = Math.floor(Math.random() * max) + 1;
    } while (exchanges.filter(x => x.id == id).length > 0);
    return id;
}
