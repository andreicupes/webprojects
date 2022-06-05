let logs = [];
let currentIndex = 0;

function showHint(str) {
    if (str == "") {
        let listContainer = document.querySelector('.logs-list1');
        listContainer.innerHTML = '';
        logs = [];
        currentIndex = 0;
    }

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("GET", "getHint.php?q=" + str, true);
    xmlhttp.send();

    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            // document.getElementById("txtHint").innerHTML = this.responseText;
            logs = JSON.parse(this.responseText);
            let listContainer = document.querySelector('.logs-list1');
            let listElement = document.createElement('ul');
            let length = logs.length < 4 ? logs.length : 4;
            console.log(listContainer)
            console.log(logs);
            for (let i = 0; i < length; i++) {
                let liElem = document.createElement('li');
                liElem.innerHTML = '<p>ID: ' + logs[i + currentIndex].id + '  |  type: ' + logs[i + currentIndex].type + '  |  severity: ' + logs[i + currentIndex].severity +
                    '  |  date: ' + logs[i + currentIndex].date + '  |  userID: ' + logs[i + currentIndex].userID + '  |  log: ' + logs[i + currentIndex].log + ' </p>';
                listElement.appendChild(liElem);
            }
            currentIndex += length;
            listContainer.appendChild(listElement);
        }
    }
}

const toLeft = document.querySelector('.goleft');
const toRight = document.querySelector('.goright');

toLeft.addEventListener('click', () => {
    if (currentIndex >= 4) {
        if (currentIndex % 4 !== 0) {
            currentIndex -= (currentIndex % 4);
        } else {
            currentIndex -= 4
        }
        let listContainer = document.querySelector('.logs-list1');
        listContainer.innerHTML = '';

        let listElement = document.createElement('ul');
        for (let i = 0; i < 4; i++) {
            let liElem = document.createElement('li');
            liElem.innerHTML = '<p>ID: ' + logs[i + currentIndex - 4].id + '  |  type: ' + logs[i + currentIndex - 4].type + '  |  severity: ' + logs[i + currentIndex - 4].severity +
                '  |  date: ' + logs[i + currentIndex - 4].date + '  |  userID: ' + logs[i + currentIndex - 4].userID + '  |  log: ' + logs[i + currentIndex - 4].log + ' </p>';
            listElement.appendChild(liElem);
        }
        currentIndex += length;
        listContainer.appendChild(listElement);

    }
});

toRight.addEventListener('click', () => {
    console.log(logs.length);
    console.log(currentIndex);
    if (currentIndex < logs.length && logs.length > 3) {
        let nextLog = logs.length - currentIndex;
        console.log(nextLog)
        let length = nextLog < 4 ? nextLog : 4;

        let listContainer = document.querySelector('.logs-list1');
        listContainer.innerHTML = '';

        let listElement = document.createElement('ul');
        for (let i = 0; i < length; i++) {
            let liElem = document.createElement('li');
            liElem.innerHTML = '<p>ID: ' + logs[i + currentIndex].id + '  |  type: ' + logs[i + currentIndex].type + '  |  severity: ' + logs[i + currentIndex].severity +
                '  |  date: ' + logs[i + currentIndex].date + '  |  userID: ' + logs[i + currentIndex].userID + '  |  log: ' + logs[i + currentIndex].log + ' </p>';
            listElement.appendChild(liElem);
        }
        currentIndex += length;
        listContainer.appendChild(listElement);
    }
});