import { UserProfile } from './post.model';

export interface CommentDto {
  id: number;
  body: string;
  userId: number;
  postId: number;
  parentCommentId?: number;
  likedByUsers?: UserProfile[];
  childComments?: CommentDto[];
  user: UserProfile;
  createdOn: Date;
  updatedOn?: Date;
}
