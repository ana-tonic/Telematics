import { Delivery } from "./delivery.js";

export class Start {
    constructor() {
        this.container = null;
        this.middleTable = null;
        this.rightTable = null;
    }

    draw(host) {
        if (!host)
            throw new Error("Host is not defined");

        this.container = document.createElement("div");
        this.container.className = "container";
        host.appendChild(this.container);

        const leftStart = document.createElement("div");
        leftStart.className = "leftStart";
        this.container.appendChild(leftStart);

        const tableDiv = document.createElement("div");
        tableDiv.className = "tableDiv";
        this.container.appendChild(tableDiv);

        this.drawMiddle(tableDiv);

        const rightTableDiv = document.createElement("div");
        this.container.appendChild(rightTableDiv);

        this.drawRightTable(rightTableDiv);

        this.drawIMG(this.container);

        this.drawLeft(leftStart);
    }

    drawIMG(host) {
        const rightStart = document.createElement("img");
        rightStart.className = "rightStart";
        rightStart.src = "Truck.png";
        host.appendChild(rightStart);
    }

    drawRightTable(host) {

        const tableDiv = document.createElement("div");
        this.rightTable = tableDiv;
        tableDiv.className = "rightTableDiv";
        host.appendChild(tableDiv);
    }

    drawRightTableButtons(host, delivery_id) {
        const buttonsDiv = document.createElement("div");
        buttonsDiv.className = "buttonsDiv";
        host.appendChild(buttonsDiv);

        const h1 = ["Truck", "Fuel", "Unit", "Reading time"];
        const h2 = ["Truck", "Time Idle", "Unit", "Reading time"];
        const h3 = ["Truck", "Speed", "Unit", "Reading time"];
        const h4 = ["Truck", "Location", "Distance", "Reading time"];

        const buttonFuel = document.createElement("button");
        buttonFuel.className = "rightButton";
        buttonFuel.innerHTML = "Fuel";
        buttonFuel.onclick = () => {
            console.log(delivery_id);
        }
        buttonsDiv.appendChild(buttonFuel);

        const buttonIdling = document.createElement("button");
        buttonIdling.className = "rightButton";
        buttonIdling.innerHTML = "Idling time";
        buttonIdling.onclick = () => {
            console.log("Idling");
        }
        buttonsDiv.appendChild(buttonIdling);

        const buttonSpeed = document.createElement("button");
        buttonSpeed.className = "rightButton";
        buttonSpeed.innerHTML = "Speed";
        buttonSpeed.onclick = () => {
            console.log("Speed");
        }
        buttonsDiv.appendChild(buttonSpeed);

        const buttonLocation = document.createElement("button");
        buttonLocation.className = "rightButton";
        buttonLocation.innerHTML = "Location";
        buttonLocation.onclick = () => {
            console.log("FuLocationel");
        }
        buttonsDiv.appendChild(buttonLocation);

        fetch(`https://localhost:5001/Deliveries/GetFuel/${delivery_id}`).then(p => {
            p.json().then(data => {

                this.drawRightTableContent(host, data, h1);
            });

        });

    }

    drawRightTableContent(host, data, h) {
        let table = document.createElement("table");
        table.className = "table";
        host.appendChild(table);


        const header = document.createElement("tr");
        table.appendChild(header);

        h.forEach(hName => {
            const c = document.createElement("th");
            c.innerHTML = hName;
            header.appendChild(c);
        });

        data.forEach(row => {
            const r = document.createElement("tr");
            table.appendChild(r);

            console.log(row);
            const c1 = document.createElement("td");
            c1.innerHTML = row[3];
            r.appendChild(c1);

            const c2 = document.createElement("td");
            c2.innerHTML = row[2];
            r.appendChild(c2);

            const c3 = document.createElement("td");
            c3.innerHTML = row[4];
            r.appendChild(c3);

            const t = new Date(row[1]);
            const tf = t.getDate() + "-" + (t.getMonth() + 1) + "-" + t.getFullYear() + " " +
                t.getHours() + ":" + t.getMinutes() + ":" + t.getSeconds();

            const c4 = document.createElement("td");
            c4.innerHTML = tf;
            r.appendChild(c4);
        });
    }

    drawMiddle(host) {

        let table = document.createElement("table");
        this.middleTable = table;
        table.className = "table";
        host.appendChild(table);
    }

    drawDeliveryHeader(table) {
        const header = document.createElement("tr");
        table.appendChild(header);

        const cargo = document.createElement("th");
        cargo.innerHTML = "Cargo";
        header.appendChild(cargo);

        const year = document.createElement("th");
        year.innerHTML = "Year";
        header.appendChild(year);

        const active = document.createElement("th");
        active.innerHTML = "Active";
        header.appendChild(active);

        const departing_time = document.createElement("th");
        departing_time.innerHTML = "Departing time";
        header.appendChild(departing_time);

        const arrival_time = document.createElement("th");
        arrival_time.innerHTML = "Arrival time";
        header.appendChild(arrival_time);

        const driver = document.createElement("th");
        driver.innerHTML = "Driver";
        header.appendChild(driver);

        const startAddress = document.createElement("th");
        startAddress.innerHTML = "Start address";
        header.appendChild(startAddress);

        const endAddress = document.createElement("th");
        endAddress.innerHTML = "End address";
        header.appendChild(endAddress);

        const truck_id = document.createElement("th");
        truck_id.innerHTML = "Truck";
        header.appendChild(truck_id);
    }

    drawDeliveryData(table, delivery) {
        const arrival = new Date(delivery.arrival_time);
        const departing = new Date(delivery.departing_time);

        const arrivalFormated = arrival.getDate() + "-" + (arrival.getMonth() + 1) + "-" + arrival.getFullYear() + " " +
            arrival.getHours() + ":" + arrival.getMinutes();

        const departingFormated = departing.getDate() + "-" + (departing.getMonth() + 1) + "-" + departing.getFullYear() + " " +
            departing.getHours() + ":" + departing.getMinutes();

        const row = document.createElement("tr");
        table.appendChild(row);
        row.onclick = () => {
            let img = this.container.querySelector("img");
            if (img != null) {
                this.container.removeChild(img);
                this.drawIMG(document.body);
            }

            console.log(delivery.delivery_id);

            const par = this.rightTable.parentNode;
            par.removeChild(this.rightTable)
            this.drawRightTable(par);
            this.drawRightTableButtons(this.rightTable, delivery.delivery_id);

        };

        const cargo = document.createElement("td");
        cargo.innerHTML = delivery.cargo;
        row.appendChild(cargo);

        const year = document.createElement("td");
        year.innerHTML = delivery.year;
        row.appendChild(year);

        const active = document.createElement("td");
        active.innerHTML = delivery.active;
        row.appendChild(active);

        const departing_time = document.createElement("td");
        departing_time.innerHTML = departingFormated;
        row.appendChild(departing_time);

        const arrival_time = document.createElement("td");
        arrival_time.innerHTML = arrivalFormated;
        row.appendChild(arrival_time);

        const driver = document.createElement("td");
        driver.innerHTML = delivery.driver;
        row.appendChild(driver);

        const startAddress = document.createElement("td");
        startAddress.innerHTML = delivery.start_address;
        row.appendChild(startAddress);

        const endAddress = document.createElement("td");
        endAddress.innerHTML = delivery.end_address;
        row.appendChild(endAddress);

        const truck_id = document.createElement("td");
        truck_id.innerHTML = delivery.truck_id;
        row.appendChild(truck_id);
    }

    drawLeft(host) {
        const name = document.createElement("div");
        name.className = "name";
        host.appendChild(name);

        const h1 = document.createElement("h1");
        h1.className = "h1";
        name.appendChild(h1);
        h1.innerHTML = "Telematics";

        const h3 = document.createElement("h3");
        h3.className = "h3";
        name.appendChild(h3);
        h3.innerHTML = "Tracking deliveries from start to finish";

        const buttonSection = document.createElement("div");
        buttonSection.className = "buttonSection";
        host.appendChild(buttonSection);

        this.drawDeliveryForm(buttonSection);

        const startDelivery = document.createElement("button");
        startDelivery.className = "startDelivery sectionbutton";
        buttonSection.appendChild(startDelivery);
        startDelivery.onclick = () => {
            console.log((this.container.querySelectorAll(".DFinput")));


        }
        startDelivery.innerHTML = "Start delivery";

        this.drawShowDeliveries(buttonSection);

        const showDelivery = document.createElement("button");
        showDelivery.className = "showDelivery, sectionbutton";
        buttonSection.appendChild(showDelivery);
        showDelivery.onclick = () => {
            const cargo = this.container.querySelector(".cargoSelect").value;
            const year = this.container.querySelector(".year").value;
            fetch(`https://localhost:5001/Deliveries/GetDeliveries/${cargo}&${year}`).then(p => {

                parent = this.middleTable.parentNode;
                parent.removeChild(this.middleTable);
                this.drawMiddle(parent);
                this.drawDeliveryHeader(this.middleTable);

                p.json().then(deliveries => {

                    deliveries.forEach(delivery => {

                        const del = new Delivery(delivery[0], delivery[1], delivery[2], delivery[3],
                            delivery[4], delivery[5], delivery[6], delivery[7], delivery[8], delivery[9]);

                        this.drawDeliveryData(this.middleTable, del);
                    });

                });

            });
        }
        showDelivery.innerHTML = "Show deliveries";
    }

    drawDeliveryForm(host) {
        const DF = document.createElement("div");
        DF.className = "DF";
        host.appendChild(DF);

        this.drawDFormElement(DF, "Truck number: ", "number", "TruckID");
        this.drawDFormElement(DF, "Driver: ", "text", "Driver");
        this.drawDFormElement(DF, "Start Address: ", "text", "StartAddres");
        this.drawDFormElement(DF, "End Address: ", "text", "EndAddress");

        const cri = document.createElement("div");
        cri.className = "elContainer";
        DF.appendChild(cri);

        const cargo = ["Iron ore",
            "Coal",
            "Cereal",
            "Salt",
            "Aluminum",
            "Copper ore"];
        let cargoSelect = document.createElement("select");
        cargoSelect.className = "select cargoStart";
        const lbl = document.createElement("label");
        lbl.innerHTML = "Cargo: ";
        cri.appendChild(lbl);

        for (let i = 0; i < cargo.length; i++) {
            const c = document.createElement("option");
            c.value = cargo[i];
            c.innerHTML = cargo[i];
            cargoSelect.appendChild(c);
        }

        cri.appendChild(cargoSelect);
    }

    drawDFormElement(host, lblText, tip, className) {
        const elContainer = document.createElement("div");
        elContainer.className = "elContainer";
        host.appendChild(elContainer);

        const label = document.createElement("label");
        label.innerHTML = lblText;
        elContainer.appendChild(label);

        const el = document.createElement("input");
        el.type = tip;
        el.className = className + " DFinput";
        elContainer.appendChild(el);
    }

    drawShowDeliveries(host) {
        const cri = document.createElement("div");
        cri.className = "cri";
        host.appendChild(cri);

        const cargo = ["Iron ore",
            "Coal",
            "Cereal",
            "Salt",
            "Aluminum",
            "Copper ore"];
        let cargoSelect = document.createElement("select");
        cargoSelect.className = "select cargoSelect";
        const lbl = document.createElement("label");
        lbl.innerHTML = "Cargo: ";
        cri.appendChild(lbl);

        for (let i = 0; i < cargo.length; i++) {
            const c = document.createElement("option");
            c.value = cargo[i];
            c.innerHTML = cargo[i];
            cargoSelect.appendChild(c);
        }

        cri.appendChild(cargoSelect);


        const y = document.createElement("div");
        y.className = "cri";
        host.appendChild(y);

        const lbly = document.createElement("label");
        lbly.innerHTML = "Year";
        y.appendChild(lbly);


        const year = document.createElement("input");
        year.type = "number";
        year.value = new Date().getFullYear();
        year.min = 2000;
        year.max = new Date().getFullYear();;
        year.className = "year select";
        y.appendChild(year);
    }


}