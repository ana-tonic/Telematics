export class Delivery {

    constructor(cargo, year, active, departing_time, delivery_id, arrival_time, driver, end_address, start_address, truck_id) {
        this.truck_id = truck_id;
        this.delivery_id = delivery_id;
        this.driver = driver;
        this.start_address = start_address;
        this.end_address = end_address;
        this.year = year;
        this.departing_time = departing_time;
        this.arrival_time = arrival_time;
        this.cargo = cargo;
        this.active = active;
    }
}