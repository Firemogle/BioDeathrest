# Deathrest Biosculptor - Current Build Notes

Target: RimWorld 1.6.4850.

- Native CompDeathrestBindable is used for Deathrest binding/capacity.
- Multiple interfaces can bind when native Deathrest capacity permits.
- The interface does not run the vanilla BiosculpterPod session, so the Deathrest pawn does not enter the interface.
- Vanilla Biosculpter nutrition storage/hauling is retained.
- VNPE PipeSystem nutrition is supported through VNPE_NutrientPasteNet.
- The selected routine starts automatically during Deathrest when 5 nutrition is available.
- Pleasure, Medic, Bioregeneration, and Age Reversal routines are supported.
- The interface uses the vanilla BiosculpterPod operating effect while active.
- The inspector does not display the bound pawn name.

Build with Visual Studio 2022 using the supplied RimWorld 1.6.4850 references.
