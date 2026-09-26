import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import {Anime} from "../models/anime";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AnimeService {
    constructor(private http: HttpClient) {}
    getAnimes(): Observable<Anime[]> {
        return this.http.get<Anime[]>('http://localhost:5278/anime')
    }
    createAnime(anime: Anime) : Observable<Anime> {
        return this.http.post<Anime>('http://localhost:5278/anime', anime);
    }
    removeAnime(id: number) {
        return this.http.delete(`http://localhost:5278/anime/${id}`);
    }
    updateAnime(id: number, anime: Anime){
        return this.http.put(`http://localhost:5278/anime/${id}`, anime);
    }
    
}