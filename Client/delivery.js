export class Delivery {

    constructor(cargo, year, active, departing_time, delivery_id, arrival_time, driver, end_address, start_address, truck_id) {
        this.truckID = truck_id;
        this.deliveryID = delivery_id;
        this.driver = driver;
        this.startAddress = start_address;
        this.endAddress = end_address;
        this.year = year;
        this.departingTime = departing_time;
        this.arrivalTime = arrival_time;
        this.cargo = cargo;
        this.active = active;
    }
}