export function convertNameToCode (name: string):string {
  return name
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .replace(/[^a-z0-9_]+/gi, '_')
    .replace(/^-|-$/g, '')
    .toLowerCase()
}
