import { MatTableDataSource } from '@angular/material/table';
import { DeleteService } from './../interfaces/delete-service';

export class GenericTableOptions<T> {
    columns: string[];
    dataSource: MatTableDataSource<T>;
    deleteService:DeleteService;
    editLink:string;
}