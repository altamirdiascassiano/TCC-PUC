import { BASE_URL } from "./base";

export default async ( url: string,  method = 'GET',  body?: any,  headers = {}): Promise<any> => {
  const controller = new AbortController();
  try {
    const res = await fetch(`${BASE_URL}${url}`, {
      method: method.toUpperCase(),
      signal: controller.signal,
      body: body !== "" ? body : undefined, //typeof body === 'object' ? JSON.stringify(body) : undefined,
      // mode: 'cors',
      headers: {
        'Content-type': 'application/json',
        // 'Authorization': API_TOKEN,
        ...headers
      }
    });
    if (!res.ok) {
      const error = await res.text();
      return { error };
    }
    return await res.text();
  } catch (err) {
    return { error: err };
  } finally {
    controller.abort();
  }
};