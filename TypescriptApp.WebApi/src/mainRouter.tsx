import * as React from 'react';
import { RouteComponentProps, Switch, Route } from 'react-router';
import * as linq from 'linq';
import { BrowserRouter } from 'react-router-dom';

type IRoutingProps = {
	linkName: string,
	link: string
	renderer?: (props: RouteComponentProps<{}>) => JSX.Element 
}

const staticRouting: IRoutingProps[] = [
	{
		link: '/posts',
		linkName: 'Posts',
	},
	{
		link: '/tasks',
		linkName: 'Tasks'
	},
	{
		link: '/friends',
		linkName: 'Friends'
	}
]

function getNavPanel(props: IRoutingProps[]): JSX.Element {
	return <ul className='nav-panel'>
		{props.map(p => <li>
			{p.linkName}
		</li>)}
	</ul>
}

function getRouting(props: IRoutingProps[]): JSX.Element {
	return <BrowserRouter>
		<Switch>
		{props.map(x => <Route exact path={x.link} render={x.renderer} />)}
		</Switch>
	</BrowserRouter>
}


export function MainRouter() {

	return <div className='layout'>
		{getNavPanel(staticRouting)}
		{getRouting(staticRouting)}
	</div>
}