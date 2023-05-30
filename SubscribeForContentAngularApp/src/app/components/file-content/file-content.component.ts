import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { FileContent } from 'src/app/models/Posts/post.model';

@Component({
  selector: 'app-file-content',
  templateUrl: './file-content.component.html',
  styleUrls: ['./file-content.component.scss'],
})
export class FileContentComponent implements OnInit {
  @Input() fileList: FileContent[] | undefined;

  miscFileList: FileContent[] = [];
  audioFileList: FileContent[] = [];
  gallery: FileContent[] = [];
  sliderList: Array<object> = [];
  constructor(private sanitizer: DomSanitizer) {}
  ngOnInit() {
    if (this.fileList) {
      this.fileList.forEach((file) => {
        file.fileType = this.checkFileType(file.extension);
        const data = 'some text';
        const blob = new Blob([data], {
          type: 'application/octet-stream',
        });
        file.fileDownloadUrl = this.sanitizer.bypassSecurityTrustResourceUrl(
          window.URL.createObjectURL(blob)
        );
      });
      this.gallery = this.fileList.filter(
        (f) => f.fileType === 'image' || f.fileType === 'video'
      );
      this.gallery.map((item) => {
        this.sliderList.push({
          image: item.url,
          thumbImage: item.url,
          alt: item.name + item.extension,
        });
      });
      this.audioFileList = this.fileList.filter((f) => f.fileType === 'audio');
      this.miscFileList = this.fileList.filter((f) => f.fileType === 'misc');
    }
  }

  checkFileType(ext: string) {
    if (/\.(gif|jpe?g|tiff?|png|webp|bmp)$/i.test(ext)) {
      return 'image';
    } else if (
      /\.(mp4|mkv|wmv|m4v|mov|avi|flv|webm|mka|m4a|aac|ogg)$/i.test(ext)
    ) {
      return 'video';
    } else if (/\.(wav|mp3|m4a|flac|wav|wma|aac)$/i.test(ext)) {
      return 'audio';
    }
    return 'misc';
  }
}
