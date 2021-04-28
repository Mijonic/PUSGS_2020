import { FileUpload } from './../../../../shared/models/file.model';
import { ToastrService } from 'ngx-toastr';
import { WorkRequestService } from './../../../../services/work-request.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { ActivatedRoute } from '@angular/router';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { HttpEventType} from '@angular/common/http';

@Component({
  selector: 'app-work-request-multimedia',
  templateUrl: './work-request-multimedia.component.html',
  styleUrls: ['./work-request-multimedia.component.css']
})
export class WorkRequestMultimediaComponent implements OnInit {
  workRequestId:number;
  @ViewChild("fileDropRef", { static: false }) fileDropEl: ElementRef;
  files: FileUpload[] = [];

  constructor(private route:ActivatedRoute, private tabMessaging:TabMessagingService, private workReqService:WorkRequestService,
    private toastr:ToastrService)
    {
      const wrId = this.route.snapshot.paramMap.get('id');
      if(wrId && wrId != "")
      {
        this.tabMessaging.showEdit(+wrId);
        this.workRequestId = +wrId;
      }
    }

  ngOnInit(): void {

  }

  onFileDropped($event: any[]) {
    this.prepareFilesList($event);
  }

  fileBrowseHandler(event:any) {
    let files = (<HTMLInputElement>event.target).files! ;
    this.prepareFilesList(Array.from(files));
  }

  deleteFile(file:FileUpload) {
    if (file.progress < 100 && !file.aborted) {
      return;
    }
    this.files = this.files.filter(x => x != file);
  }

  uploadFiles(files:FileUpload[]){
    files.forEach(fileUpload => {
      this.workReqService.uploadAttachment(fileUpload.file, this.workRequestId).subscribe( (event) => {
        if (event.type === HttpEventType.UploadProgress) {
          fileUpload.progress = Math.round(100 * event.loaded / event.total!);
          if(fileUpload.progress == 100)
          {
            this.toastr.success(`File ${fileUpload.file.name} has been uploaded!`);
          }
        }
      }, (error) => {
        if(error.error instanceof ProgressEvent)
        {
          this.toastr.error("File can't be uploaded, server is unreachable");
        }else
        {
          this.toastr.error(`Error occured uploading file ${fileUpload.file.name} ${error.error}`); 
        }
        fileUpload.progress = 0;
        fileUpload.aborted = true;
        this.deleteFile(fileUpload);
      });

    });
  
  }

  prepareFilesList(files:  File[]) {
    let filesToUpload:FileUpload[] = [];
    for (const item of files) {
      let fileUpload = new FileUpload();
      fileUpload.file = item;
      fileUpload.progress = 0;
      this.files.push(fileUpload);
      filesToUpload.push(fileUpload);
    }
    this.fileDropEl.nativeElement.value = "";
    this.uploadFiles(filesToUpload);
  }

  formatBytes(bytes: number, decimals = 2) {
    if (bytes === 0) {
      return "0 Bytes";
    }
    const k = 1024;
    const dm = decimals <= 0 ? 0 : decimals;
    const sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + " " + sizes[i];
  }


}
