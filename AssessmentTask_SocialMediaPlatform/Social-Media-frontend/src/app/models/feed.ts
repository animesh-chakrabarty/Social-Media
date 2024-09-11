import { Post } from './post';
import { Like } from './like';
import { Comment } from './comment';

export interface Feed {
  posts: Post[];
  likes: Like[];
  comments: Comment[];
}
