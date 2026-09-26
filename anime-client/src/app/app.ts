import { Component} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AnimeService } from './services/anime.service';
import {Anime} from './models/anime';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  imports: [RouterOutlet, ReactiveFormsModule],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  animes: Anime[] = [];
  editingAnimeId: number = 0;
  newAnime: Anime = {
    id: 0,
    title: "",
    episodes: 0,
    rating: 0.00
  }
  constructor(private animeService: AnimeService) {
    this.animeService.getAnimes().subscribe(data=> {
      this.animes = data;
    });
  }
  addAnime() {
        if(this.form.invalid) return;
        this.newAnime.title = this.form.value.title ?? "";
        this.newAnime.rating = this.form.value.rating ?? 0;
        this.newAnime.episodes = this.form.value.episodes ?? 0;
        
        this.animeService.createAnime(this.newAnime).subscribe(data => {
          this.animes.push(data);
          this.form.reset({title: "", episodes: 0, rating: 0});
    });
  }
  deleteAnime(id: number) {
    this.animeService.removeAnime(id).subscribe(() => {
      this.animes = this.animes.filter(anime => anime.id !== id);
    }); 
  }
  editAnime(anime: Anime) {
    this.editingAnimeId = anime.id;
    this.form.patchValue({
      title: anime.title,
      episodes: anime.episodes,
      rating: anime.rating
    });
  }
  updateAnime() {
    if(this.form.invalid) return;
    const updatedAnime: Anime = {
      id: this.editingAnimeId,
      title: this.form.value.title ?? "",
      episodes: this.form.value.episodes ?? 0,
      rating: this.form.value.rating ?? 0
    };
    console.log(this.editingAnimeId);
console.log(updatedAnime);
    this.animeService.updateAnime(this.editingAnimeId, updatedAnime).subscribe(() => {
      this.animes = this.animes.map(anime => 
        anime.id === this.editingAnimeId ? updatedAnime : anime
      );
      this.editingAnimeId = 0;
      this.form.reset({title: "", episodes: 0, rating: 0});
    });
  }
  form = new FormGroup({
    title: new FormControl("",
       [Validators.required,
       ]),
    episodes: new FormControl(0,
       [
        Validators.min(1),
       ]),
    rating: new FormControl(0,
       [
        Validators.min(1),
        Validators.max(10)
       ])
  })
}
