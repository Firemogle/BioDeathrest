# RimWorld 1.6.4850 API audit

The source was audited against the supplied RimWorld 1.6.4850 Assembly-CSharp.dll.

- No `Thing.AllComps` usage remains.
- No `Thing.GetComp<T>()` usage remains; component access uses `TryGetComp<T>()`.
- Native `CompDeathrestBindable`, `CompBiosculpterPod`, and related components are used where appropriate.
- Reflection is retained only where native visibility/signatures make hard-coded access undesirable.

The supplied references are the authoritative development assemblies for this project.
