export class Start {
    constructor() {
        this.container = null;
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

        const rightStart = document.createElement("img");
        rightStart.className = "rightStart";
        rightStart.src = "Truck.png";
        this.container.appendChild(rightStart);

        this.drawLeft(leftStart);
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
            console.log(year);
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
        year.max = 2040;
        year.className = "year select";
        y.appendChild(year);
    }


}