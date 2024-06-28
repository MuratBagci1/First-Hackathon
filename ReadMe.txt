vid1: Unity TextureImporter ayarlandı
vid2: Gerekli assetler, SFX ve Audio indirildi
vid3: Input ve Cinemachine package'ları indirildi, play mode kapatıldı
vid4: Background ve Layer'lar ayarlandı
vid5: karakter eklendi (character prefeb), 
	*Unity'e bağlı olan herhangi bir oyun objesi Monobehaviour sınıfından kalıtılır,
	bu objeye belli başlı fonksiyonlar kazandırır.
	*Play modunda yapılan değişiklikler oyuna etki etmez.
vid6: Cinemachine ayarlandı, kamera ve karakter takip ayarlandı
vid7: Parallax effect ile background animasyonları ayarlandı. Parallax Effect: https://www.youtube.com/watch?v=tMXgLBwtsvI
	*eklenen video'yu izleyip değerlerle oynayarak arka planın daha yavaş hareket etmesini sağlayabilirsin
vid8: yürüme, koşma ve boş durma animasyonları ayarlandı, sample rate ayarlandı, flip ayarlandı. Tuş atamaları yapıldı
	*neden sprite yerine scale değiştirilir öğren.
vid9: Managing Animator Parameters with Static Strings
vid10: Ground Tiles ayarlandı, gravity + capsule ve composite collider'lar eklendi
vid11: grounded ve falling ayarlamaları yapıldı, jump eklendi
	*double jump ve reinforced jump eklenebilir!
vid12: attack ayarlandı, attack animasyonları eklendi (attack 2 ve attack 3 doğru çalışmıyor)
	*attack animasyonunda facedirection ve combo attackları ayarla!
vid13: knight enemy eklendi (assetler splice'lanacak x:0.47, y:0.23), 2 yeni background asset eklendi (extra)
	*zıplayınca yön değiştirme problemini çöz(problem rb2d continious collision ile çözüldü, onWall kodunda hata olabilir)
vid14: Knight animasyonları eklendi, knight animator controller eklendi, detection zone eklendi, yavaşlayarak durma eklendi!
	*yavaşlayarak durmayı diğer karakterlere de ekleyebilirsin
vid15: ölüm animasyonları ayarlandı, ölünce silinme ayarlandı
vid16: Knight attack ve hit animasyonları yapıldı
	*saldırırken sürüklenme kapatıldı
	*hit animasyonu bool'dan trigger'a çevrildi (bende bool da düzgün çalışıyor, istediğin zaman updateleyebilirsin)
	*hit için iyileştirme: track the number of hits to each target on attack limit to 1 or more hits for swing
vid17: knight için attack cooldown eklendi, Cliff Detection Zone eklendi
vid18: UI elementleri eklendi (damage ve health), UI manager eklendi
	*WorldToScreenPoint araştır!
	*UI elementleri Canvas'a assign edilmelidir
vid19: healing item eklendi (pick up item)
vid20: attack combo eklendi, lockVelocity sorunu çözüldü(hit state'ine canMove setboolbehaviour script'i eklendi)
vid21: bow eklendi
	*attack için yön değiştirmeyi durdur!
	*rangedattack objesi boş
vid22: flying enemy eklendi
	*flying enemy hit animation çöz
vid23: air attack'lar eklendi, fixed transition duration kapatıldı
	*player'ı havada saldırı yapınca y ekseninde dondur, aa3 ve loop'u ayarla
vid24: #if --- #endif context specified if statement, build gives scenes an integer value as index
vid25: audio (müzik) eklendi
	*ses ayarlarına bakmak isteyebilirsin
vid26: scale with screen size, health bar eklendi,
	*setfloat sorununu çöz (Unity)


