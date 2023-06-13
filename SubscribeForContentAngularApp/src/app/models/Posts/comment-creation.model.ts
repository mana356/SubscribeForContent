export interface CommentCreationDto {
  Body: string;
  PostId: number;
  ParentCommentId?: number;
}
