export interface UserProfileUpdateDto {
  Name: string;
  UserName: string;
  DateOfBirth?: Date;
  Bio?: string;
  IsACreator: boolean;
  ProfilePicture?: File;
  CoverPicture?: File;
}
