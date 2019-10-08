Public Class Form1
    Public Class MyPictureBox
        Inherits PictureBox
        Public Property linha As Integer
        Public Property coluna As Integer
        Public Property minas As Integer
        Public Property bomba = False
        Public Property clicada = False
    End Class
    Dim Quadro(9, 9) As MyPictureBox
    Sub airstrike()
        Dim bombas = 0
        Dim l, c As Integer
        Do
            l = Int(Rnd() * 9)
            c = Int(Rnd() * 9)
            If Not Quadro(l, c).bomba Then
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


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For li = 0 To 8
            For co = 0 To 8
                Quadro(li, co) = New MyPictureBox
                Quadro(li, co).Name = "quadro" + Str(li) + Str(co)
                Quadro(li, co).linha = li
                Quadro(li, co).coluna = co
                Quadro(li, co).BackgroundImageLayout = ImageLayout.Stretch
                Quadro(li, co).BorderStyle = BorderStyle.FixedSingle
                Quadro(li, co).BackColor = Color.AliceBlue
                Quadro(li, co).Location = New Point(80 + 66 * co, 50 + 66 * li)
                Quadro(li, co).Size = New Size(64, 64)
                Quadro(li, co).Visible = True
                AddHandler Quadro(li, co).Click, AddressOf Clicar
                Me.Controls.Add(Quadro(li, co))
            Next
        Next
        Call airstrike()
        For li = 0 To 8
            For co = 0 To 8
                Call preenche(li, co)
            Next
        Next
    End Sub
    Sub Clicar(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If sender.clicada Then : Return
        Else : sender.clicada = True
        End If
        sender.BackgroundImage = Nothing
        sender.BackColor = Nothing
        sender.BorderStyle = Nothing

        If sender.bomba Then
            sender.BackgroundImage = My.Resources.Mine
        Else
            Select Case sender.minas
                Case 1 : sender.BackgroundImage = My.Resources._1
                Case 2 : sender.BackgroundImage = My.Resources._2
                Case 3 : sender.BackgroundImage = My.Resources._3
                Case 4 : sender.BackgroundImage = My.Resources._4
                Case 5 : sender.BackgroundImage = My.Resources._5
                Case 6 : sender.BackgroundImage = My.Resources._6
                Case 7 : sender.BackgroundImage = My.Resources._7
                Case 8 : sender.BackgroundImage = My.Resources._8
            End Select

        End If
    End Sub
End Class
