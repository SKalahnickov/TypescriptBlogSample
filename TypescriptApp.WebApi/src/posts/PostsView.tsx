import * as React from 'react';
import './PostsView.scss';
import { RouteComponentProps } from 'react-router';
import { SimpleLoader } from '../loaders/loader'
import * as moment from 'moment'


type IPost = {
	id: number,
	title: string,
	category: string,
	createdAt: moment.Moment,
	createdBy: string,
	text: string
}

const existingPosts: IPost[] = [
	{
		id: 1,
		title: "Why do dogs bark?",
		category: "Whatever",
		createdAt: moment.utc(new Date()),
		createdBy: 'J. Brown',
		text: 'Because they are silly'
	},
	{
		id: 2,
		title: "Why do dogs bark?",
		category: "Whatever",
		createdAt: moment.utc(new Date()),
		createdBy: 'J. Brown',
		text: 'Because they are silly'
	}
]

type IPostsLoaderProps = {
	redirect: (url: string) => void
}

export function PostsLoader({ redirect }: IPostsLoaderProps) {

	const [posts, setPosts] = React.useState<IPost[] | null>(null);
	return posts === null
		? <SimpleLoader
			loader={http => http.get("api/posts/list")}
			onSuccess={(posts: IPost[]) => setPosts(posts)}
			onError={_ => null}
			timeout={30000}
		/>
		: <PostsView
		redirect={redirect}
		posts={posts}
	/>
}


type IPostsViewProps = IPostsLoaderProps & {
	posts: IPost[]
}

export function PostsView({ redirect, posts }: IPostsViewProps) {
	console.log("Posts", posts);
	return <div className='posts-container'>
		{posts.map(x => <PostModelView {...x} onClick={() => redirect(`posts/${x.id}`)} />)}
	</div>

}

type IPostModelView = IPost & {
	onClick: () => void
}

function PostModelView({ category, createdAt, createdBy, id, onClick, text, title }: IPostModelView) {
	return <div className='post-model'>
		<div className='post-header'>{category}</div>
		<div className='post-body'>{title}</div>
		<div className='post-bottom'>
			<div className='created-by'>
				<span>by </span>
				<span>{createdBy}</span>
			</div>
			<div className='created-at'>{createdAt === undefined
				? createdAt
				: new Date(createdAt.toString()).toLocaleDateString() /* fix date serialization */}</div>
		</div>
		</div>
}