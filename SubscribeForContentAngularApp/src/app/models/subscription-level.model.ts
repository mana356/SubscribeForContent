import { UserProfile } from 'firebase/auth';

export interface SubscriptionLevelDto {
  id: number;
  levelName: string;
  levelPrice: number;
  levelDescription: string;
  creator: UserProfile;
}
