

export class Report{
  constructor(data){
    this.id = data.id
    this.title = data.title
    this.body = data.body
    this.pictureOfDisgust = data.pictureOfDisgust
    this.creatorId = data.creatorId
    this.creator = data.creator
    // REVIEW optional just as a cool thing to show off
    this.restaurant = data.restaurant
  }
}
