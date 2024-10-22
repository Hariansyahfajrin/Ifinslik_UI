function PreviewFile(file, target) {
  const tab = window.open()
  console.log(file)
  tab.document.body.innerHTML = `<img src=${file} width="100%" height="100%" />`
}