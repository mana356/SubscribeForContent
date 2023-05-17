export interface Post {
  Id: number;
  Title: string;
  Description: string;
  ImageUrl?: string;
  Content?: string;
  CreatorId: number;
  CreatorName: string;
  CreatedDate: Date;
  IsLiked?: boolean;
}
