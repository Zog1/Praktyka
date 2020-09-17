import Api from "./API";

export default class LocationApi extends Api {
  async fetchLocalization(reservationid) {
    try {
      const res = await this.baseAxios.get("/locations/" + reservationid);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  async setLocalization(props) {
    try {
      await this.baseAxios.post("/locations/", props);
    } catch (error) {
      console.log(error);
    }
  }

  async deleteLocalization(reservationid) {
    try {
      await this.baseAxios.delete("/locations/", reservationid);
    } catch (error) {
      console.log(error);
    }
  }
}
