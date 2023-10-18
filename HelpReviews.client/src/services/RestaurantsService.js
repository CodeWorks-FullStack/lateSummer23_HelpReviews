import { AppState } from "../AppState.js";
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


}

export const restaurantsService = new RestaurantsService();