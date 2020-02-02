import * as React from 'react';
import './mainRouter.scss';
import { RouteComponentProps, Switch, Route, Redirect } from 'react-router';
import * as linq from 'linq';
import { BrowserRouter, NavLink } from 'react-router-dom';
import { PostsLoader } from './posts/PostsView';

type IRoutingProps = {
	linkName: string,
	link: string
	renderer?: (props: RouteComponentProps<{}>) => JSX.Element 
}

const staticRouting: IRoutingProps[] = [
	{
		link: '/posts',
		linkName: 'Posts',
		renderer: props => <PostsLoader redirect={props.history.push} />

	},
	{
		link: '/tasks',
		linkName: 'Tasks',
		renderer: _ => <div>Tasks are comming soon</div>
	},
	{
		link: '/friends',
		linkName: 'Friends',
		renderer: _ => <div>Friends are almost released</div>
	}
]

function GetRouteRenderer(
	allLinks: IRoutingProps[],
	currentLink: string,
	routeProps: RouteComponentProps<{}>
) {
	const link = allLinks.find(x => x.link === currentLink)!;

	return <React.Fragment>
		<ul className='nav-panel'>
			{allLinks.map(p => <NavLink key={`nav-link_${p.link}`} to={p.link}><li
				className={p.link === currentLink ? 'nav-active' : 'nav-inactive'}
				>
				{p.linkName}
			</li></NavLink>)}
		</ul>
		{link.renderer === undefined
			? null
			: link.renderer(routeProps)}
	</React.Fragment>
}

export function MainRouter() {
	return <div className='navigation'>
		<BrowserRouter>		
			<Switch>
				{staticRouting.map(x => <Route
					path={x.link}
					render={props => GetRouteRenderer(staticRouting, x.link, props)}
					key={x.link}
					exact
				/>)}
				<Route path="/" key="default" render={_ => <Redirect to={staticRouting[0].link} />} />
			</Switch>
		</BrowserRouter>
	</div>
}