import { Component, OnInit } from '@angular/core';
import { AddCommand, UpdateCommand, TenderDTO, TenderService } from 'src/app/tender-management-api';
import { DBOperation } from 'src/app/shared/enum';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { UtilService } from 'src/app/shared/util.service';

interface SelectValue {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-manage-tender',
  templateUrl: './manage-tender.component.html',
  styleUrls: ['./manage-tender.component.css']
})
export class ManageTenderComponent implements OnInit {
  modalTitle: string;
  modalBtnTitle: string;
  dbops: DBOperation;
  tender: TenderDTO;
  minDate: Date;
  maxDate: Date;

  tenderForm: FormGroup = this.fb.group({
    id: [''],
    name: ['', [Validators.required, Validators.max(64)]],
    contractNumber: ['', [Validators.required, Validators.max(64)]],
    releaseDate: [''],
    closingDate: [''],
    description: ['', [Validators.max(128)]],
  });

  constructor(private utilService: UtilService, private tenderService: TenderService, private fb: FormBuilder, public dialogRef: MatDialogRef<ManageTenderComponent>) {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 10, 0, 1);
    this.maxDate = new Date(currentYear + 10, 11, 31);
  }

  ngOnInit(): void {
    if (this.dbops != DBOperation.create)
      this.tenderForm.patchValue(this.tender);

    if (this.dbops == DBOperation.delete)
      this.tenderForm.disable();

    if (this.dbops == DBOperation.update) {
      // this.tenderForm.controls["name"].disable();
    }
  }

  onSubmit() {
    switch (this.dbops) {
      case DBOperation.create:
        if (this.tenderForm.valid) {
          this.tenderService.post(<AddCommand>{
            name: this.tenderForm.value.name,
            contractNumber: this.tenderForm.value.contractNumber,
            releaseDate: this.tenderForm.value.releaseDate,
            closingDate: this.tenderForm.value.closingDate,
            description: this.tenderForm.value.description
          }).subscribe(
            data => {
              if (data > 0) {
                this.utilService.openSnackBar("Successfully added the Tender!");
                this.dialogRef.close()
              }
              else {
                this.utilService.openSnackBar("Error adding Tender, contact your system administrator!");
              }
            }
          );
        }
        break;
      case DBOperation.update:
        if (this.tenderForm.valid) {
          this.tenderService.put(<UpdateCommand>{
            id: this.tenderForm.value.id,
            name: this.tenderForm.value.name,
            contractNumber: this.tenderForm.value.contractNumber,
            releaseDate: this.tenderForm.value.releaseDate,
            closingDate: this.tenderForm.value.closingDate,
            description: this.tenderForm.value.description
          }).subscribe(
            data => {
              if (data == true) {
                this.utilService.openSnackBar("Successfully updated the Tender!");
                this.dialogRef.close()
              }
              else {
                this.utilService.openSnackBar("Error updating Tender, contact your system administrator!");
              }
            }
          );
        }
        break;
      case DBOperation.delete:
        this.tenderService.delete(this.tenderForm.value.id).subscribe(
          data => {
            if (data == true) {
              this.utilService.openSnackBar("Successfully deleted the Tender!");
              this.dialogRef.close()
            }
            else {
              this.utilService.openSnackBar("Error deleting tender, contact your system administrator!");
            }
          }
        );
        break;
    }
  }
}