export interface PostCreationDto {
  Title: string;
  Description: string;
  FileContents?: File[];
  Content?: string;
  CreatorSubscriptionLevelId: number;
}
