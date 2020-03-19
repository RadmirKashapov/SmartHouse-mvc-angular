import { Injectable } from "@angular/core"
import { HttpClient } from "@angular/common/http"
import { House } from "./house"
import { Record } from "./record"
import { Room } from "./room"
import { Sensor } from "./sensor"
import { User } from "./user"

@Injectable
export class DataService {

    private houseUrl = "/api/houses";
    private recordUrl = "/api/records";
    private roomUrl = "/api/rooms";
    private sensorUrl = "/api/sensors";

    constructor(private http: HttpClient) { }

    getHouses() {
        return this.http.get(this.houseUrl);
    }

    getRooms() {
        return this.http.get(this.roomUrl);
    }

    getRecords() {
        return this.http.get(this.recordUrl);
    }

    getSensors() {
        return this.http.get(this.sensorUrl);
    }

    getHouse(id: number) {
        return this.http.get(this.houseUrl + "/" + id);
    }

    getRoom(id: number) {
        return this.http.get(this.roomUrl + "/" + id);
    }

    getRecord(id: number) {
        return this.http.get(this.recordUrl + "/" + id);
    }

    getSensor(id: number) {
        return this.http.get(this.sensorUrl + "/" + id);
    }

    createHouse(house: House) {
        return this.http.post(this.houseUrl, house);
    }

    createRoom(room: Room) {
        return this.http.post(this.roomUrl, room);
    }

    createRecord(record: Record) {
        return this.http.post(this.recordUrl, record);
    }

    createSensor(sensor: Sensor) {
        return this.http.post(this.sensorUrl, sensor);
    }

    updateHouse(house: House) {

        return this.http.put(this.houseUrl, house);
    }

    updateRoom(room: Room) {

        return this.http.put(this.roomUrl, room);
    }

    updateRecord(record: Record) {

        return this.http.put(this.recordUrl, record);
    }

    updateSensor(sensor: Sensor) {

        return this.http.put(this.sensorUrl, sensor);
    }

    deleteHouse(id: number) {
        return this.http.delete(this.houseUrl + '/' + id);
    }

    deleteRoom(id: number) {
        return this.http.delete(this.roomUrl + '/' + id);
    }

    deleteRecord(id: number) {
        return this.http.delete(this.recordUrl + '/' + id);
    }

    deleteSensor(id: number) {
        return this.http.delete(this.sensorUrl + '/' + id);
    }
}