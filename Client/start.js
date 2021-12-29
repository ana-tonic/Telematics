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
        startDelivery.className = "startDelivery, sectionbutton";
        buttonSection.appendChild(startDelivery);
        startDelivery.innerHTML = "Start delivery";

        const showDelivery = document.createElement("button");
        showDelivery.className = "showDelivery, sectionbutton";
        buttonSection.appendChild(showDelivery);
        showDelivery.innerHTML = "Show deliveries";
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
        el.className = className + ", DFinput";
        elContainer.appendChild(el);
    }


    drawDeliveryForm(host) {
        const DF = document.createElement("div");
        DF.className = "DF";
        host.appendChild(DF);

        this.drawDFormElement(DF, "Truck number: ", "number", "TruckID");
        this.drawDFormElement(DF, "Driver: ", "text", "Driver");
        this.drawDFormElement(DF, "Start Address: ", "text", "StartAddres");
        this.drawDFormElement(DF, "End Address: ", "text", "EndAddress");
        this.drawDFormElement(DF, "Cargo: ", "text", "Cargo");
    }
}