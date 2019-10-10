Public Class Form1
    Public Class MyPictureBox
        Inherits PictureBox
        Public Property linha As Integer
        Public Property coluna As Integer
        Public Property minas As Integer
        Public Property bomba = False
        Public Property flag = False
        Public Property clicada = False
    End Class
    Dim Quadro(9, 9) As MyPictureBox
    Dim start = False

    Sub airstrike()
        Dim bombas = 0
        Dim l, c As Integer
        Do
            l = Int(Rnd() * 9)
            c = Int(Rnd() * 9)
            If Not Quadro(l, c).bomba And Not Quadro(l, c).clicada Then
                Quadro(l, c).bomba = True
                bombas += 1
            End If
        Loop While bombas < 10
    End Sub
    Sub preenche(r, c)
        If Quadro(r, c).bomba Then : Return
        End If

        Dim bombas = 0
        For x = r - 1 To r + 1
            For y = c - 1 To c + 1
                If x >= 0 And x < 9 And y >= 0 And y < 9 Then
                    If Quadro(x, y).bomba Then : bombas += 1
                    End If
                End If
            Next
        Next
        Quadro(r, c).minas = bombas
    End Sub

    Sub Inicializa()
        For li = 0 To 8
            For co = 0 To 8
                Quadro(li, co).BorderStyle = BorderStyle.FixedSingle
                Quadro(li, co).BackColor = Color.LightGray
                Quadro(li, co).BackgroundImage = Nothing
                Quadro(li, co).minas = 0
                Quadro(li, co).bomba = False
                Quadro(li, co).flag = False
                Quadro(li, co).clicada = False
            Next
        Next
        start = False
    End Sub
    Sub VerSeGanhou()
        Dim c = 0
        For li = 0 To 8
            For co = 0 To 8
                If Quadro(li, co).clicada Then : c += 1
                End If
            Next
        Next
        If c > 70 Then
            MsgBox("Parabéns!!! Novo Jogo?",, "Fim do Jogo")
            Call Inicializa()
        End If
    End Sub

    Sub IniciarBombas()
        Call airstrike()
        For li = 0 To 8
            For co = 0 To 8
                Call preenche(li, co)
            Next
        Next
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For li = 0 To 8
            For co = 0 To 8
                Quadro(li, co) = New MyPictureBox
                Quadro(li, co).Name = "quadro" + Str(li) + Str(co)
                Quadro(li, co).linha = li
                Quadro(li, co).coluna = co
                Quadro(li, co).BackgroundImageLayout = ImageLayout.Stretch
                Quadro(li, co).Location = New Point(80 + 66 * co, 60 + 66 * li)
                Quadro(li, co).Size = New Size(64, 64)
                Quadro(li, co).Visible = True
                AddHandler Quadro(li, co).MouseClick, AddressOf Clicar
                Me.Controls.Add(Quadro(li, co))
            Next
        Next
        Call Inicializa()

    End Sub
    Sub Clicar(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        If sender.clicada Then : Return
        End If

        If sender.flag Then
            sender.BackgroundImage = Nothing
            sender.flag = False
            Return
        End If

        If e.Button = MouseButtons.Right Then
            sender.BackgroundImage = My.Resources.Flag
            sender.flag = True
            Return
        End If

        Call Jogar(sender.linha, sender.coluna)
        Call VerSeGanhou()
    End Sub
    Sub Jogar(r, c)
        Dim aux = Quadro(r, c)
        aux.clicada = True
        aux.BackgroundImage = Nothing
        aux.BackColor = Color.White
        aux.BorderStyle = Nothing

        If Not start Then
            start = True
            Call IniciarBombas()
        End If

        If aux.bomba Then
            aux.BackgroundImage = My.Resources.Mine
            Beep()
            MsgBox("Bombaaaa!!! Novo Jogo?",, "Game Over")
            Call Inicializa()
            Return
        Else
            Select Case aux.minas
                Case 1 : aux.BackgroundImage = My.Resources._1
                Case 2 : aux.BackgroundImage = My.Resources._2
                Case 3 : aux.BackgroundImage = My.Resources._3
                Case 4 : aux.BackgroundImage = My.Resources._4
                Case 5 : aux.BackgroundImage = My.Resources._5
                Case 6 : aux.BackgroundImage = My.Resources._6
                Case 7 : aux.BackgroundImage = My.Resources._7
                Case 8 : aux.BackgroundImage = My.Resources._8
            End Select
        End If

        If Quadro(r, c).minas = 0 Then
            For x = r - 1 To r + 1
                For y = c - 1 To c + 1
                    If x >= 0 And x < 9 And y >= 0 And y < 9 Then
                        If Not Quadro(x, y).clicada Then : Call Jogar(x, y)
                        End If
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Beep()
        Dim resposta = MsgBox("Tem a certeza?", vbYesNo, "Novo Jogo")
        If resposta = vbNo Then Return
        Call Inicializa()
    End Sub
End Class
