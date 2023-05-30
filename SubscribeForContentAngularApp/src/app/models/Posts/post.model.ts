export interface Post {
  id: number;
  title: string;
  description: string;
  content?: string;
  createdOn: Date;
  creator: UserProfile;
  subscriptionLevel?: any;
  likedByUsersCount: number;
  postCommentsCount: number;
  fileContents?: FileContent[];
}

export interface UserProfile {
  id: number;
  name: string;
  firebaseUserId: string;
  userName?: string;
  email: string;
  dateOfBirth?: Date;
  bio?: string;
  createdOn: Date;
  isACreator: boolean;
  profilePicture: any;
}

export interface FileContent {
  id: number;
  name: string;
  type: string;
  extension: string;
  url: string;
  postId?: number;
  userProfilePictureId?: number;
  userCoverPictureId?: number;
  fileType?: string;
  fileDownloadUrl?: any;
}
