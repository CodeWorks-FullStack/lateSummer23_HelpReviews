import { AppState } from "../AppState.js";
import { Report } from "../models/Report.js";
import { Restaurant } from "../models/Restaurant.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class RestaurantsService {

    async getRestaurants() {
        const res = await api.get('api/restaurants')
        logger.log('[GET RESTAURANTS]', res.data)
        AppState.restaurants = res.data.map(d => new Restaurant(d))
        logger.log('[APPSTATE RESTAURANTS]', AppState.restaurants)

    }

    async getRestaurantDetails(restaurantId){
      const res = await api.get('api' + '/' + 'restaurants' + '/' + `${restaurantId}`)
      logger.log('Got restaurant by id', res.data)

      AppState.activeRestaurant = new Restaurant(res.data)
    }

    async getRestaurantReports(restaurantId){
      const res = await api.get(`api/restaurants/${restaurantId}/reports`)
      logger.log('Reports📃',res.data)
      AppState.reports = res.data.map(r => new Report(r))
    }

    async updateRestaurant(restaurant){
      const res = await api.put(`api/restaurants/${restaurant.id}`, restaurant)
      logger.log('Updated restaurant', res.data)

      AppState.activeRestaurant = new Restaurant(res.data)
    }

}

export const restaurantsService = new RestaurantsService();
