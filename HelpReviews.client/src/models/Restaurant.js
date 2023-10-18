import { Profile } from "./Profile.js"

export class Restaurant {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.imgUrl = data.imgUrl
        this.description = data.description
        this.visits = data.visits
        this.isShutdown = data.isShutdown
        this.creatorId = data.creatorId
        this.creator = new Profile(data.creator)
    }
}