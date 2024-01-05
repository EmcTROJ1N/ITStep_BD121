export interface ICrudController
{
  OnGetAsync(): any;
  OnPostAsync(item: any): any;
  OnDeleteAsync(id: string | number): any;
  OnPutAsync(id: string | number, item: any): any;
}

export default class FetchController implements ICrudController
{
    #Url: string;
    constructor(url: string, ) {
        this.#Url = url;
    }

    async OnGetAsync()
    {
        let response = await fetch(this.#Url);
        return await response.json();
    }

    async OnDeleteAsync(id: string | number)
    {
        const request = await fetch(`${this.#Url}/${id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
            },
        });
        return request;
    }

    async OnPostAsync(item: any)
    {
        const request = await fetch(this.#Url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(item),
        });
        return await request.json();
    }

    async OnPutAsync(id: string, item: any)
    {
        const request = await fetch(`${this.#Url}/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(item),
        });
        return request;
    }
}
