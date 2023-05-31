import { FileContent } from './Posts/post.model';

export interface UserProfile {
  id: number;
  name: string;
  userName: string;
  email: string;
  dateOfBirth?: Date;
  bio?: string;
  createdOn: Date;
  isACreator: boolean;
  profilePicture?: FileContent;
  coverPicture?: FileContent;
  TotalSubscribers?: number;
}
