import { TableControlRadioOptions } from './table-control-radio-options.model';
import { SearchFilterService } from './../interfaces/search-filter-service';
export class TableControlOptions {
    shouldInitSearch:boolean = true;
    shouldInitFilter:boolean = true;
    shouldInitRadio:boolean = true;
    shouldInitSaveButton:boolean = true;
    filterValues:string[] | null= [];
    isMultiFilter:boolean | null= true;
    buttonNaviLink:string | null = "/";
    controlService:SearchFilterService;
    radioOptions:TableControlRadioOptions | null;
}
