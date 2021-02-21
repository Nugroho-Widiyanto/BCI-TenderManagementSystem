import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { TenderDTO, TenderService} from 'src/app/tender-management-api';
import { DBOperation } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material/dialog';
import { ManageTenderComponent } from '../manage-tender/manage-tender.component';
import { UtilService } from 'src/app/shared/util.service';

@Component({
  selector: 'app-list-tenders',
  templateUrl: './list-tenders.component.html',
  styleUrls: ['./list-tenders.component.css']
})

export class ListTendersComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = ['name', 'contractNumber', 'releaseDate', 'closingDate', 'description','edit','delete'];
  data: MatTableDataSource<TenderDTO>; 

  constructor(private tenderService:TenderService, public dialog: MatDialog, private util: UtilService) { }

  ngOnInit(): void {
    this.loadTender();
  }

  private loadTender(){
   this.tenderService.getAll().subscribe(tender => {
     this.data = new MatTableDataSource(tender.tenderList);
     this.data.paginator = this.paginator;
     this.data.sort = this.sort;
   })
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.data.filter = filterValue.trim().toLowerCase();
    if (this.data.paginator) {
      this.data.paginator.firstPage();
    }
  }

  private openManageTender(tender: TenderDTO = null, dbops:DBOperation, modalTitle:string, modalBtnTitle:string)
  {
    let dialogRef = this.util.openCrudDialog(this.dialog, ManageTenderComponent, '70%');
    dialogRef.componentInstance.dbops = dbops;
    dialogRef.componentInstance.modalTitle = modalTitle;
    dialogRef.componentInstance.modalBtnTitle = modalBtnTitle;
    dialogRef.componentInstance.tender = tender;
 
     dialogRef.afterClosed().subscribe(result => {
         this.loadTender();
     });
  }

  public addTender()
  {
    this.openManageTender(null, DBOperation.create, 'Add New Tender', 'Add');
  }

  public editTender(tender: TenderDTO)
  {
    this.openManageTender(tender, DBOperation.update, 'Update Tender', 'Update');
  }

  public deleteTender(tender: TenderDTO)
  {
    this.openManageTender(tender, DBOperation.delete, 'Delete Tender', 'Delete');
  }
}