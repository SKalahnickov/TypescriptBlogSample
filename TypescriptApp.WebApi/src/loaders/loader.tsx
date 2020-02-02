import * as React from 'react';
import axios, * as Axios from 'axios';

type ILoaderProps<T> = {
	loader: (http: Axios.AxiosInstance) => Promise<T>,
	onSuccess: (model: T) => void,
	onError: (message: string) => void,
	timeout: number
}

export function loaderState<T>({ loader, onSuccess, onError, timeout }: ILoaderProps<T>) {

	const [success, setSuccess] = React.useState<T | null>(null);
	const [fail, setFail] = React.useState<string | null>(null);

	const setResult = React.useMemo(() => (s: T | null, f: string | null) =>{
		setSuccess(s);
		setFail(f);
	}, [setSuccess, setFail]);

	React.useEffect(function () {
			let cancel: Axios.Canceler;
			const cancellation = new axios.CancelToken(canceller => cancel = canceller);
		const client = axios.create({
			validateStatus: status => status === 401 || (status >= 200 && status < 300),
			cancelToken: cancellation
		});
			const timeoutId = setTimeout(() => cancel(`Время ожидания ответа истекло`), timeout);
			loader(client)
				.catch(err => typeof err === "string"
					? setResult(null, err as string)
					: setResult(null, "Unknown error"))
				.then((v: any) => {
					if (typeof v === 'object')
						setResult(v.data as T, null);

				});
			return () => {
				clearTimeout(timeoutId);
				cancel("Request canceled");
			}
	}, [loader, onSuccess, onError, timeout])

	React.useEffect(function () {
		if (fail !== null)
			onError(fail);
		if (success !== null)
			onSuccess(success);
	}, [loader, onSuccess, onError, timeout]);

}

export function SimpleLoader<T>({ loader, onSuccess, onError, timeout }: ILoaderProps<T>) {
	const [isLoading, setLoading] = React.useState(true);
	
	const req = React.useMemo(() => async (http: Axios.AxiosInstance) => {
		setLoading(true);
		return await loader(http);
	}, [loader, setLoading]);

	loaderState<T>({
		loader: req,
		onSuccess: value => { setLoading(false); onSuccess(value); },
		onError: value => { setLoading(false); onError(value); },
		timeout: timeout		
	});

	return isLoading ? <div>'Loading...'</div> : null; // throbber to come
}