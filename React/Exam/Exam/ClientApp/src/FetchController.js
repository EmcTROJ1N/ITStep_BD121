export class FetchController
{
    #Url;
    constructor(url) {
        this.#Url = url;
    }
    
    async OnGetAsync()
    {
        let response = await fetch(this.#Url);
        return await response.json();
    }
    
    async OnDeleteAsync(id)
    {
        const request = await fetch(`${this.#Url}/${id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
            },
        });
        return request;
    }
    
    async OnPostAsync(item)
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
    
    async OnPutAsync(id, item)
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