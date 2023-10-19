
import {api} from './AxiosService'
import {logger} from '../utils/Logger.js'


class ReportsService{

  async  createReport(reportData){
    const res = await api.post('api/reports', reportData)
    logger.log('Created Report', res.data)

  }

}

export const reportsService = new ReportsService()
